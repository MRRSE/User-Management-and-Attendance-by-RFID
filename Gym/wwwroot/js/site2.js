
async function up() {
  gg = false;
  // گرفتن مقادیر ورودی از فرم
  let name = document.getElementById("name").value;
  let lname = document.getElementById("lname").value;
  let age = document.getElementById("age").value;
  let classes = document.getElementById("classes").value;
  let Pnumber = document.getElementById("number").value;
  let calssesConverted = Number(classes);
  let alert = document.getElementById("alert");
  let radios = document.getElementsByName("gender");
  let genderValue = "";
      // گرفتن جنسیت
  for (let i = 0; i < radios.length; i++) {
    if (radios[i].checked) {
      genderValue = radios[i].value;
      break;
    }
  }

    // بررسی پر بودن همه فیلدها
    if (
      name === "" || lname === "" || age === "" ||
      classes === "" || genderValue === "" || Pnumber === ""
    ) 
    {
      alert.innerText = "لطفا اطلاعات خواسته‌ شده را کامل وارد کنید.";
      alert.style.color = "brown";
      gg = true;
      return;
    }

    document.getElementById("card-message").style.display = "flex";
    startCountdown(10);

    function startCountdown(durationInSeconds){
      const countdownE1 = document.getElementById("countdown");
      let timeLeft = durationInSeconds;

      countdownE1.innerText = timeLeft;

      const timer = setInterval(() => {
        timeLeft--;
        countdownE1.innerText = timeLeft;
        if(timeLeft <= 0){
          clearInterval(timer);
        }
      }, 1000)
    }

  // دریافت UID کارت
  const cardUID = await waitForUid(); // متوقف می‌شود تا کارت خوانده شود

  // بررسی خوانده شدن کارت
  if (!cardUID) {
    document.getElementById("card-message").style.display="none";
    alert.innerText = "کارت شناسایی خوانده نشد. لطفاً دوباره تلاش کنید.";
    alert.style.color = "brown";
    return;
  }
  // ارسال اطلاعات با axios
  axios({
    url: "/home/newPerson",
    method: "post",
    params: {
      name: name,
      lname: lname,
      age: age,
      classes: calssesConverted,
      gender: genderValue,
      Pnumber: Pnumber,
      cardUID: cardUID,
    }
  })
    .then(res => {
      if(res.data == "card mojod ast"){
        alert.innerText = "این کارت قبلا ثبت شده است";
        alert.style.color = "red";
        gg = true;
        return;
      }
      else{
        alert.style.color = "green";
        console.log("پاسخ سرور:", res.data);
        let newId = "fdf";
        alert.innerText = "ثبت‌نام انجام شد . شناسه کاربری : " + res.data ;
        gg = true;
      }
    })
    .catch(err => {
      alert.innerText = "خطا در ثبت‌نام. لطفاً دوباره تلاش کنید.";
      alert.style.color = "red";
      console.error("خطا:", err);
      gg = true;
    });
}

// تابعی که منتظر UID کارت می‌ماند
async function waitForUid(timeoutMs = 10000, intervalMs = 1000) {
  const start = Date.now();

  while (Date.now() - start < timeoutMs) {
    try {
      console.log("منتظر کارت...");
      const res = await fetch("http://localhost:5048/api/rfid");
      const uid = await res.text();

      if (uid && uid.trim() !== "") {
        document.getElementById("overlay").style.visibility = "visible";
        setTimeout(() => {
          document.getElementById("overlay").style.visibility = "hidden";
        }, 2000);
        console.log(uid.trim());
          document.getElementById("card-message").style.display="none";
        return uid.trim();
      }
    } catch (err) {
      console.error("خطا در دریافت UID:", err);
      gg = true;
    }

    await new Promise(resolve => setTimeout(resolve, intervalMs));
  }

  // اگر زمان تمام شد و UID دریافت نشد
  console.log("هیچ UID‌ای خوانده نشد.");
  gg = true;
  return null;
}


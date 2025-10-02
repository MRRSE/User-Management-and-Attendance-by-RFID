
let message;
let gg = true;

    setInterval(async () => {
        try {
            if(gg){
                const res = await fetch("http://localhost:5048/api/rfid");
                const uid = await res.text();
        
                if (uid) {
                    axios({
                        url:"/Home/SingByUid",
                        method:"post",
                        params:{uid : uid}
                    })
                    .then(res => { 
                        console.log(res.data)
                        message = res.data;
                        showMessage()
                    })
                    .catch(err => console.log(err))
                }       
            }
    
        } catch (err) {
            console.error("❌ خطا در دریافت UID:", err);
        }
    }, 500);


function showMessage(text, type = "success") {
    
    const box = document.getElementById("messageBox");
    box.innerText = message;
  
    box.style.display = "block";
    box.style.padding = "0px 0px"
    box.style.opacity = "0";

    setTimeout(()=> {
        box.style.padding="40px 30px"
        box.style.opacity ="1";
    },50)

    setTimeout(()=> {
        box.style.padding = "0px 0px"
        box.style.opacity ="0";

        setTimeout(() => {
            box.style.display = "none";
          }, 3000);
    },3000)
  }
  

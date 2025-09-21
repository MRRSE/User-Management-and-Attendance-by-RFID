

document.addEventListener('DOMContentLoaded', () => {
    const input1 = document.getElementById('UsernameInp');
    const input2 = document.getElementById('Sing-inp2');
    const button = document.getElementById('singBtn');
  
    function checkInputs() {
      if (input1.value.trim() === "" || input2.value.trim() === "") {
        button.classList.add('disabled'); 
        button.disabled = true; 
      } else {
        button.classList.remove('disabled'); 
        button.disabled = false; 
      }
    }
  
    input1.addEventListener('input', checkInputs);
    input2.addEventListener('input', checkInputs);
  
    checkInputs();
  });
  
  function SingIn(){
    let userName = document.getElementById("UsernameInp").value
    let password = document.getElementById("Sing-inp2").value

    document.getElementById("overlay").style.visibility = "visible";

    setTimeout(hideLoader, 2000); 

    axios({
      url : "../home/checkUser",
      method : "get",
      params : {userName : userName , password : password}
    })
    .then(res => {
      if (res.data.success) {
        if(res.data.role === 1) {
          window.location.href = "Home/index2";
        }
      } else {
          alert("نام کاربری یا رمز عبور اشتباه است.");
      }
    })
    .catch(err =>{
      console.log(err.message)
      alert("نام کاربری یا رمز عبور اشتباه است.");
    } 
    
    )
    function hideLoader() {
      document.getElementById("overlay").style.visibility = "hidden";
  }
  }
  document.addEventListener("keydown", function(event) {
    if (event.key === "Enter") {
        document.getElementById("singBtn").click();
    }
});

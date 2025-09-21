function newPass() {
    let userName = document.getElementById("userNameInp").value
    let firstPass = document.getElementById("passInp").value
    let secPass = document.getElementById("secPassInp").value
    let id = 1;

    document.getElementById("overlay").style.visibility = "visible";

    setTimeout(hideLoader, 2000);
    if(userName === "" || firstPass === "" || secPass === ""){
        console.log("اطلاعات خواسته شده را به درستی وارد کنید");
        let successMes = document.getElementById("messageBox")
        successMes.innerText = "اطلاعات را به درستی وارد کنید";
        successMes.style.display = "block";
        successMes.style.padding = "0px 0px"
        successMes.style.opacity = "0";

        setTimeout(() => {
            successMes.style.padding = "40px 30px"
            successMes.style.opacity = "1";
        }, 50)

        setTimeout(() => {
            successMes.style.padding = "0px 0px"
            successMes.style.opacity = "0";

            setTimeout(() => {
                successMes.style.display = "none";
            }, 3000);
        }, 3000)
        document.getElementById("edit-box").style.display = "none";
        return
    }
    else if (firstPass == secPass){
        axios({
            url : "/home/newPass",
            method : "post",
            params : {password : firstPass , username : userName , id : id}
        })
        .then(res => {
            console.log(res.data)
            let successMes = document.getElementById("messageBox")
            successMes.innerText = res.data;
            successMes.style.display = "block";
            successMes.style.padding = "0px 0px"
            successMes.style.opacity = "0";

            setTimeout(() => {
                successMes.style.padding = "40px 30px"
                successMes.style.opacity = "1";
            }, 50)

            setTimeout(() => {
                successMes.style.padding = "0px 0px"
                successMes.style.opacity = "0";

                setTimeout(() => {
                    successMes.style.display = "none";
                }, 3000);
            }, 3000)
            document.getElementById("userNameInp").value="";
            document.getElementById("passInp").value="";
            document.getElementById("secPassInp").value="";
        })
        .catch(err => console.log(err))
    }
    else {
        let successMes = document.getElementById("messageBox")
        successMes.innerText = "رمز عبور یکی نیست!";
        successMes.style.display = "block";
        successMes.style.padding = "0px 0px"
        successMes.style.opacity = "0";

        setTimeout(() => {
            successMes.style.padding = "40px 30px"
            successMes.style.opacity = "1";
        }, 50)

        setTimeout(() => {
            successMes.style.padding = "0px 0px"
            successMes.style.opacity = "0";

            setTimeout(() => {
                successMes.style.display = "none";
            }, 3000);
        }, 3000)
        document.getElementById("edit-box").style.display = "none";
    }
    function hideLoader() {
        document.getElementById("overlay").style.visibility = "hidden";
    }

}

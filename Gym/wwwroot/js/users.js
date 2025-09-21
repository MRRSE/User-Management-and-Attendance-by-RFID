let take = 10;
let skip = 0;

function load() {
    let filterValue = document.getElementById("genderFilter").value;

    if ( filterValue === "all" ){
        axios({
            url: "/home/getData",
            method: "get",
            params: { take: take, skip: skip }
        })
            .then(res => {
                f1(res.data);
                skip += take;
            })
            .catch(err => console.log(err.message));
    }
    else if(filterValue === "مرد"){
        axios({
            url: "/home/getmen",
            method: "get",
            params: {take: take , skip : skip , filterValue : filterValue}
        })
        .then(res => {
            f1(res.data);
            skip += take;
        })
        .catch(err => console.log(err.message));
    }
    else if(filterValue === "زن"){
        axios({
            url: "/home/getwomen",
            method: "get",
            params: {take: take , skip: skip , filterValue: filterValue}
        })
        .then(res => {
            f1(res.data);
            skip += take;
        })
        .catch(err => console.log(err.message))
    }
    else if(filterValue === "جلسات"){
        axios({
            url: "../home/GetExpiredUsers",
            method:"get",
            params:{take:take , skip: skip}
        })
        .then(res => {
            if(res.data) {
                f1(res.data); // همون تابعی که داری برای نمایش لیست
            }
            skip += take;
        })
        .catch(err => {
            console.log("کاربری با جلسات صفر پیدا نشد");
        });
        
    }
}

function f1(list) {
    let t = document.querySelector("#data-table tbody");
    list.forEach(x => {
        let item = document.createElement("tr");

        let cell1 = document.createElement("th");
        cell1.textContent = x.id;
        cell1.className = "row1";
        let cell2 = document.createElement("th");
        cell2.textContent = x.name;
        let cell3 = document.createElement("th");
        cell3.textContent = x.lname;
        cell2.className = "row1";
        let cell4 = document.createElement("th");
        cell4.textContent = x.number;
        let cell5 = document.createElement("th");
        cell5.textContent = x.gender;
        cell5.className = "row1";
        let cell6 = document.createElement("th");
        cell6.textContent = x.Clases;

        let cell7 = document.createElement("th");
        cell7.textContent = x.classes;
        cell7.classList = "classes";
        let cell9 = document.createElement("th");
        cell9.textContent = x.date ? x.date.trim() : "تاریخ نامشخص";

        let cell8 = document.createElement("th");
        cell8.textContent = x.usertype;
        cell8.classList = "classes";

        cell9.className = "row1";
        
        

        item.appendChild(cell1);
        item.appendChild(cell2);
        item.appendChild(cell3);
        item.appendChild(cell4);
        item.appendChild(cell5);
        item.appendChild(cell7);
        item.appendChild(cell8);
        item.appendChild(cell9);

        t.appendChild(item);
    });
}

//فیلتر جنسیت
function filterGender() {
    let filterValue = document.getElementById("genderFilter").value;
    let rows = document.querySelectorAll("#data-table tbody tr");
    let t = document.querySelector("#data-table tbody");
    t.innerHTML = "";
    skip = 0;
    console.log(skip)

    rows.forEach(row => {
        let genderCell = row.querySelector("th:nth-child(5)"); // ستون جنسیت
        if (filterValue === "all") {
        } else if (filterValue === "male") {
            axios({

            })
        } else if (filterValue === "female" && genderCell.textContent === "زن") {
            
        } else {
            
        }
    });
}

function find() {
    let searchValue = document.getElementById("searchInp").value.trim();

    if (searchValue === "") {
        alert("لطفا شناسه مورد نظر را وارد کنید")
        return;
    }
    if (!isNaN(searchValue)){
        axios({
            url: "/home/findUser",
            method: "get",
            params: { id : searchValue }
        })
            .then(res => {
                let t = document.querySelector("#data-table tbody");
                t.innerHTML = "";
    
                // اضافه کردن نتیجه جستجو به جدول
                if (res.data) {
                    f1([res.data]); // استفاده از همان تابع f1 برای نمایش کاربر
                } else {
                    alert("کاربری با این شناسه پیدا نشد.");
                }
            })
            .catch(err => console.log(err))
    }
    else {
        axios({
            url:"../home/findByName",
            method:"post",
            params: { name : searchValue }
        })
        .then(res => {
            if(res.data) {
                f1(res.data)
            }
            else {
                console.log("کاربر یافت نشد");
            }
        })
        .catch(err => {
            console.log(err.message)
            alert("کاربر یافت نشد.")
        })
    }
}   
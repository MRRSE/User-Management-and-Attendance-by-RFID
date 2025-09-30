let take = 10;
let skip = 0;

function load()
{
    axios({
        url:"/home/getlogss",
        method:"get",
        params:{take : take , skip : skip}
    })
    .then(res => {
        console.log(res)
        creatRow(res.data)
        skip += take;
    })
    .catch(err=>console.log(err))
}

function creatRow(list){
    let t = document.querySelector("#data-table tbody");
    list.forEach( z => {
        let item = document.createElement("tr");

        let cell1 = document.createElement("th");
        cell1.textContent= z.userid;
        cell1.className= "row1";

        let cell2 =document.createElement("th");
        cell2.textContent= z.name;
        cell2.className= "row1";

        let cell3 = document.createElement("th");
        cell3.textContent= z.lname;
        cell3.className= "row1";

        let cell4 = document.createElement("th");
        cell4.textContent= z.enteryDate;
        cell4.className= "row1";

        let cell5 = document.createElement("th");
        cell5.textContent= z.exitDate;
        cell5.className = "row1";

        item.appendChild(cell1);
        item.appendChild(cell2);
        item.appendChild(cell3);
        item.appendChild(cell4);
        item.appendChild(cell5);

        t.appendChild(item);

    });
}
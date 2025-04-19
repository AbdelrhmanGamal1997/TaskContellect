////////////////// signlR 
const contactId = document.getElementById("ContactId").value; 
console.log(contactId);

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/ContactHub")
    .build();

connection.on("ReceiveContactUpdate", function (contact) {
    alert("Contact has been updated!");
    console.log(contact);
    
    debugger;
    // Update form fields
    document.getElementById("Name").value = contact.name;
    document.getElementById("Address").value = contact.address;
    document.getElementById("Phone").value = contact.phone;
    document.getElementById("Notes").value = contact.notes;
});

connection.start().then(function () {
    connection.invoke("JoinContactGroup", contactId);
});

window.addEventListener("beforeunload", function () {
    connection.invoke("LeaveContactGroup", contactId);
});
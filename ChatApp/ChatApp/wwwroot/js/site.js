var createRoomBtn = document.getElementById("addRoomBtn");
var createRoomModal = document.getElementById("createRoomPopUp");


createRoomBtn.addEventListener('click', function () {
    createRoomModal.classList.add('active')
});

var closeModal = function() {
    createRoomModal.classList.remove('active');
}
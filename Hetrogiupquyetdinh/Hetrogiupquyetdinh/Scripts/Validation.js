/// <reference path="Validation.js" />
function IsTruongEmpty() {
    if (document.getElementById('truongID').value == "") {
        return 'Vui lòng chọn trường!';
    }
    else { return ""; }
}
function IsKhoiEmpty() {
    if (document.getElementById('khoiID').value == "") {
        return 'Vui lòng chọn khối!';
    }
    else { return ""; }
}
//function IsFirstNameInValid() {
//    if (document.getElementById('TxtFName').value.indexOf("@") != -1) {
//        return 'First Name should not contain @';
//    }
//    else { return ""; }
//}
//function IsLastNameInValid() {
//    if (document.getElementById('TxtLName').value.length >= 5) {
//        return 'Last Name should not contain more than 5 character';
//    }
//    else { return ""; }
//}
function IsSoThichEmpty() {
    if (document.getElementById('Id').value == "") {
        return 'Vui lòng chọn sở thích!';
    }
    else { return ""; }
}
function IsDiemEmpty() {
    if (document.getElementById('Diem').value == "") {
        return 'Vui lòng chọn điểm số của bạn!';
    }
    else { return ""; }
}
function IsDiemInValid() {
    if (isNaN(document.getElementById('Diem').value)) {
        return 'Điểm không hợp lệ.Điểm là một số trong khoảng từ 15 đến 30!';
    }
    else { return ""; }
}
function IsDiemLengthInValid() {
    if (parseFloat(document.getElementById('Diem').value) > 30 || parseFloat(document.getElementById('Diem').value) < document.getElementById("diemtruong").value) {
        return 'Điểm cần lớn hơn hoặc bằng điểm chuẩn của trường là '+document.getElementById("diemtruong").value+' và nhỏ hơn 30';
    }
    else { return ""; }
}

function IsValid() {

    var TruongEmptyMessage = IsTruongEmpty();
    var KhoiEmptyMessage = IsKhoiEmpty();
    var SoThichEmptyMessage = IsSoThichEmpty();
    var DiemEmptyMessage = IsDiemEmpty();
    var DiemInvalidMessage = IsDiemInValid();
    var IsDiemLengthInValidMessage = IsDiemLengthInValid();

    var FinalErrorMessage = "Errors:";
    if (TruongEmptyMessage != "")
        FinalErrorMessage += "\n" + TruongEmptyMessage;
    if (KhoiEmptyMessage != "")
        FinalErrorMessage += "\n" + KhoiEmptyMessage;
    if (SoThichEmptyMessage != "")
        FinalErrorMessage += "\n" + SoThichEmptyMessage;
    if (DiemEmptyMessage != "")
        FinalErrorMessage += "\n" + DiemEmptyMessage;
    if (DiemInvalidMessage != "")
        FinalErrorMessage += "\n" + DiemInvalidMessage;
    if (IsDiemLengthInValidMessage != "")
        FinalErrorMessage += "\n" + IsDiemLengthInValidMessage;
   

    if (FinalErrorMessage != "Errors:") {
        alert(FinalErrorMessage);
        return false;
    }
    else {
        return true;
    }
}
function successMessage(message, url) {

    Swal.fire({
        title: "Success!",
        text: message,
        icon: "success",
        showDenyButton: false,
        showCancelButton: false,
        confirmButtonText: "Ok",
    }).then((result) => {
        if (result.isConfirmed && url != undefined) {
            location.href = url;
        }
    });
}

function successMsg(message) {

    Swal.fire({
        title: "Success!",
        text: message,
        icon: "success",
        showDenyButton: false,
        showCancelButton: false,
        confirmButtonText: "Ok",
    });
}

function infoMessage(message) {

    Swal.fire({
        title: "Balance Check",
        text: message,
        icon: "info",
        showDenyButton: false,
        showCancelButton: false,
        confirmButtonText: "Ok",
    });
}


function warningMessage(message) {
    Swal.fire({
        title: "Warning!",
        text: message,
        icon: "warning",
    });
}

function errorMessage(message) {
    Swal.fire({
        title: "Error!",
        text: message,
        icon: "error",
    });
}


function validateMsg(message) {
    Notiflix.Notify.warning(message);
}

function sucessMsgNotiflixWithUrl(data, url) {
    if (data.isSuccess) 
        Notiflix.Notify.success(data.message);
    
    else 
        Notiflix.Notify.failure(data.message);
    

    if(url != undefined) 
        location.href = url;
    
}

function showMsg(data) {
    if (data.isSuccess) {
        successMsg(data.message);
    }
    else {
        errorMessage(data.message);
    }
}

function errorMsgNotiflix(message) {
    Notiflix.Notify.failure(message);
}

function showInfoMessage(data) {
    if (data.isSuccess) {
        infoMessage(data.message);
    }
    else {
        errorMessage(data.message);
    }

}



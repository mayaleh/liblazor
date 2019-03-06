function DestroyModal() {

    $(".modal-backdrop").remove();
    $("body").removeClass("modal-open");
    return true;
}

//window.DestroyModal
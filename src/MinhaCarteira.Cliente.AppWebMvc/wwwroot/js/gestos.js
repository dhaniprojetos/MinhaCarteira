document.getElementById('wipearea').addEventListener('touchstart', function (event) {
    touchstartX = event.changedTouches[0].screenX;
    touchstartY = event.changedTouches[0].screenY;
}, false);

document.getElementById('wipearea').addEventListener('touchend', function (event) {
    touchendX = event.changedTouches[0].screenX;
    touchendY = event.changedTouches[0].screenY;
    handleGesture();
}, false);

function handleGesture() {
    if (touchendX < touchstartX) {
        //console.log('Swiped Left');
    }

    if (touchendX > touchstartX) {
        //console.log('Swiped Right');
        exibirMenuLatereal();
    }

    if (touchendY < touchstartY) {
        //console.log('Swiped Up');
    }

    if (touchendY > touchstartY) {
        //console.log('Swiped Down');
    }

    if (touchendY === touchstartY) {
        //console.log('Tap');
    }
}

function exibirMenuLatereal() {
    //console.log('Swiped Right');
    //$("body").removeClass("sidebar-collapse");
    //$("body").addClass("sidebar-open");
    $('[data-widget="pushmenu"]').PushMenu('toggle');
}

function ocultarMenuLateral() {
    $('[data-widget="pushmenu"]').PushMenu('collapse');
}
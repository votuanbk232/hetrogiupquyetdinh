

$(document).ready(function(){
    var submit = $('#btn-result');
    var result = $('#client-result-final');
    var detail = $('#detail');
    var view = $('#btn-detail');
    detail.hide();
    result.hide();

    submit.on('click',function(){
        result.show('slow')
    });

    view.on('click', function(){
        detail.toggle();
    })
});
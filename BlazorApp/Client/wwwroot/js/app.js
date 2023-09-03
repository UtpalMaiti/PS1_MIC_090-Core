"use strict";

(function () {

    fetch('https://dummyjson.com/products/1')
        .then(res => res.json())
        .then(json => console.log(json))

    $(document).ready(function () {
        bootbox.alert('This is the default alert!');
    });
    $(document).ready(function () {
        bootbox.alert('This is the default alert!');
    });

  

}());
function toggleClass(el) {
    //alle genres op 'niet actief' zetten
    var ulElement = el.parentElement;
    var ilElementen = ulElement.children;
    var i;
    for (i = 0; i < ilElementen.length; i++)
    {
        ilElementen[i].className = "";
    }

    //het gekozen genre op 'actief' zetten
    el.className = "active";
    var mededeling = document.getElementById("kiesgenre");
    mededeling.style.display = "none";
}


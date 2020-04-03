const airlines=[];
$(document).ready(function () {
    console.debug("populate airlines called");
    $.ajax({
      url:"/api/airlines"

    }).then((airlines) => {
        $('#airlineSelect').append(new Option('',''));
        $.each(airlines, (i, airline) => {
            console.debug("inside loop");
            console.debug(airline);//why is it showing password?
            airline = new Airline(airline.Id, airline.AirLine_Name, airline.CountryCode);
            var option = document.createElement('option');
            option.text = airline.AirLine_Name;
            option.value = airline.Id + ':' + airline.CountryCode;         
           $('#airlineSelect').append(new Option(option.text, option.value));
        })
    }).catch((err)=>{console.error(err)})
});
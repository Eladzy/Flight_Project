const flights = [];
$tableFlights = $("#flightsTable");
jQuery(document).ready(function () {
    console.debug("loading table")
    $tableFlights = $("#flightsTable")
    $.ajax({
        url: "/api/flights"
        

    }).then((flights =>  fillTable(flights))).catch((err) => { console.log(err) })
})

    function fillTable(flights) {
        console.debug("fill..");
        console.debug(flights);
       
        $.each(flights, (i, flight) => {
            flight = new Flight(flight.Id, flight.AirLine_Id, flight.Origin_Country_Code, flight.Destination_Country_Code, flight.Landing_Time, flight.Departure_Time, undefined)
            console.debug(flight + "\n +++debug " + typeof (flight) + " +++debug")         
            $tableFlights.append("<tr>" +
                "<td>" + flight.AirLine_Id + "</td>" +
                "<td>" + flight.Origin_Country_Code + "</td>" +
                "<td>" + flight.Destination_Country_Code + "</td>" +
                "<td>" + flight.Departure_Time + "</td>" +
                "<td>" + flight.Landing_Time + "</td>" +
                "<td>" + flight.status + "</td>" +
                "</tr>")
        })
    }


//$(document).ready(() => {
//    $(`#searchBtn`).click(function () {
//        let filteredFlights =flights;
       
//        console.debug("search called");
//        console.debug(flights)
//        const flight_id = $("#flightid").val();
//        const air_company_id = $("#airlineSelect").val().split(':')[0];
//        const origin_country = $("#originSelect").val();
//        const dest_country = $("#destSelect").val();
//        const flight_radio = $(`input[name=flightType]`).val();
//        console.log(filteredFlights);
//        filteredFlights = flight_id != undefined || flight_id != "" ? filteredFlights.filter(f => f.Id == flight_id) : filteredFlights;
//        filteredFlights = air_company_id != undefined || air_company_id != "" ? filteredFlights.filter(f => f.a == air_company_id) : filteredFlights;
//        filteredFlights = origin_country != undefined || origin_country != "" || origin_country != 0
//            ?filteredFlights.filter(f => f.Origin_Country_Code == origin_country) : filteredFlights;
//        filteredFlights = dest_country != undefined || dest_country != "" || dest_country != 0
//            ?filteredFlights.filter(f => f.Destination_Country_Code == dest_country) : filteredFlights;
//    });
//});

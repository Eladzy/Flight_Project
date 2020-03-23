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

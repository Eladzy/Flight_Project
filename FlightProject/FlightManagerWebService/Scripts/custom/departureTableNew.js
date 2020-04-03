const flights = [];
jQuery(document).ready(function () {
    $tableFlights = $("#flightsTable");
    console.debug("loading table")
    $tableFlights = $("#flightsTable")
    $.ajax({
        url: "/api/flights"


    }).then((flights => fillTable(flights))).catch((err) => { console.log(err) })
})

function fillTable(flights) {
    console.debug("fill..");
    console.debug(flights);

    $.each(flights, (i, flight) => {
        flight = new jFlight(flight.Id, flight.AirLineName, flight.Origin_Country_Name, flight.Destination_Country_Name, flight.LandingTime, flight.DepartureTime, undefined)
        // flight = new Flight(flight.Id, flight.AirLine_Id, flight.Origin_Country_Code, flight.Destination_Country_Code, flight.Landing_Time, flight.Departure_Time, undefined)
        console.debug(flight + "\n +++debug " + typeof (flight) + " +++debug")
        $tableFlights.append("<tr>" +
            "<td>" + flight.AirLineName + "</td>" +
            "<td>" + flight.Origin_Country_Name + "</td>" +
            "<td>" + flight.Destination_Country_Name + "</td>" +
            "<td>" + flight.DepartureTime + "</td>" +
            "<td>" + flight.LandingTime + "</td>" +
            "<td>" + flight.status + "</td>" +
            "</tr>")
    })
}

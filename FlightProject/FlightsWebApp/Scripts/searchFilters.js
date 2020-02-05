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



$("#searchBtn").click(function () {
    console.debug("search called")
    const flight_id = $("#flightid").val()
    const air_company_name = $("#airlineSelect").val()// fetch
    const origin_country = $("#originoption").val()
    const dest_country = $("#destoption").val()
    const dep_time = $("#deptime").val()
    const land_time = $("#landtime").val()
    $tableFlights = ("#flightsTable")
    $.ajax({
        //url:`api/search/${air_company_name}/`
        url: `api/search?`,//${(air_company_name != null?'origin='+origin_country+'&':'' )}`
        type: "GET",
        data:
            { "flightID": flight_id, "airlineName": air_company_name, "originCountry": origin_country, "destCountry": dest_country, "depTime": dep_time, "landTime": land_time },
        //success: fillTable(flights).catch((err) => { console.log(err) })
    }).then((flights => fillTable(flights))).catch((err) => { console.error(err) })
});
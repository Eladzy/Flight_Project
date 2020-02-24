﻿const flights = []
$(document).ready(() => {
    $(`#searchBtn`).click(function () {
        console.debug("clicked")
        console.debug("search called")
        const flight_id = $("#flightid").val()
        const air_company_id = $("#airlineSelect").val().split(':')[0]
        const origin_country = $("#originSelect").val()
        const dest_country = $("#destSelect").val()
        const flight_radio = $(`input[name=flightType]`).val()
        const departure_time1 = null
        const departure_time2 = null
        const landing_time1 = null
        const landing_time2 = null

        switch (flight_radio) {
            case "depChecked":
                departure_time1 = new Date()
                departure_time2 = new Date()
                departure_time2 = _time2.setHours(landing_time.getHours()+ 12)
                break
            case "landChecked":
                landing_time1 = new Date()
                landing_time2 = new Date()
                landing_time2.setHours(landing_time.getHours() + 12)
                break
            default:
                departure_time1 = new Date()
                departure_time2 = new Date()
                departure_time2 = _time2.setHours(landing_time.getHours() + 12)
                landing_time1 = new Date()
                landing_time2 = new Date()
                landing_time2.setHours(landing_time.getHours() + 12)
                break
        }
        $.ajax({
            dataType: `jason`,
            url: "api/searchFlightRange",
            type: 'GET',
            data: {
                flightId = flight_id,
                airlineId=air_company_id,
                originCountryId= origin_country,
                destinationCountryId=dest_country,
                depTime1=departure_time1,
                depTime2=departure_time2,
                landTime1=landing_time1,
                landTime2=landing_time2
            }

        }).then(data, function () {
            flights.append(data)
            console.debug(flights)
            let departureTimeFilter = new Date()
            departureTimeFilter.setHours(departureTimeFilter.getHours() + 12)
          
           

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
        }).catch((err) => { console.error(err) })
    });
});
            //filteredFlights = flight_id != undefined || flight_id != "" ? filteredFlights.filter(f => f.Id == flight_id) : filteredFlights;
            //filteredFlights = air_company_id != undefined || air_company_id != "" ? filteredFlights.filter(f => f.a == air_company_id) : filteredFlights;
            //filteredFlights = origin_country != undefined || origin_country != "" || origin_country != 0
            //    ? filteredFlights.filter(f => f.Origin_Country_Code == origin_country) : filteredFlights;
            //filteredFlights = dest_country != undefined || dest_country != "" || dest_country != 0
            //    ? filteredFlights.filter(f => f.Destination_Country_Code == dest_country) : filteredFlights;
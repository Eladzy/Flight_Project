class Flight {
    constructor(id, airlineid, origincountry, destcountry, landingtime,departuretime, status) {
        this.Id = id
        this.AirLine_Id  = airlineid
        this.Origin_Country_Code = origincountry
        this.Destination_Country_Code = destcountry
        this.Departure_Time = departuretime
        this.Landing_Time = landingtime
        if (status == undefined) {
            this.status = Date(departuretime) < Date.now()?"Landing":"Scehduled"
        }
    }
}

class Airline {
    constructor(id, name, countryId) {
        this.Id = id
        this.AirLine_Name = name
        this.CountryCode =countryId
    }
}
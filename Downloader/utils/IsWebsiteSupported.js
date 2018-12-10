const args = process.argv
const axios = require("axios")

var url = args[2]

axios.head(url)
    .then(function (response) {

        // console.log(response.status);
        // console.log(response.headers);

        //file can be downlaoded
        if ("accept-ranges" in response.headers && "content-length" in response.headers) {
            //write the response to a file!
            console.log("true")
        }
        //file cant be downlaoded
        else {
            throw ("This Website is not supported!");
        }

    })
    .catch(function (error) {
        console.log("false")
    });
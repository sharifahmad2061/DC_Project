//return content-length & fileName

const args = process.argv
const axios = require("axios")
var mime = require('mime-types')

var url = args[2]

axios.head(url)
    .then(function (response) {

        // console.log(response.status);
        // console.log(response.headers);

        var filename;
        try {

            try {
                //get from Content-disposition header
                let headerLine = response.headers['content-disposition']
                let startFileNameIndex = headerLine.indexOf('"') + 1
                let endFileNameIndex = headerLine.lastIndexOf('"')
                filename = headerLine.substring(startFileNameIndex, endFileNameIndex)
            } catch (err) {

                try {
                    //get from url.
                    var tmp_url = url.split("?")[0];
                    if (tmp_url.substr(tmp_url.lastIndexOf('/') + 1).split(".")[1] == mime.extension(response.headers["content-type"])) {
                        filename = tmp_url.substr(tmp_url.lastIndexOf('/') + 1).split(".")[0] + "." + mime.extension(response.headers["content-type"]);
                    } else {
                        throw err;
                    }
                } catch (err) {
                    throw err;
                }

            }

        } catch (Exception) {
            //generate a random name
            filename = uuidv4() + "." + mime.extension(response.headers["content-type"])
        }

        //file can be downlaoded
        if ("accept-ranges" in response.headers && "content-length" in response.headers) {
            //write the response to a file!
            // console.log("true")
            console.log(filename + ";" + response.headers["content-length"])
        }
        //file cant be downlaoded
        else {
            throw ("This Website is not supported!");
        }

    })
    .catch(function (error) {
        console.log("false")
    });


function uuidv4() {
    //src: https://stackoverflow.com/a/2117523
    return 'xxxxxxxxxxxx4xx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0,
            v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
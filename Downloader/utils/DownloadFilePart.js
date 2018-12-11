const axios = require("axios")
const fs = require('fs')
const args = process.argv
const partId = uuidv4()

//Create TMP if doesn't exsist
if (fs.existsSync("tmp") == false) {
    console.log("Creating Temp Dir")
    fs.mkdirSync("./tmp");
}

//now download the file
axios.get(args[2], {
    responseType: 'stream',
    headers: {
        Range: `bytes=${args[3]}-${args[4]}`
    }
}).then(function (res) {

    if (206 == res.status && "content-range" in res.headers) {
        // console.log(res.headers)
        res.data.pipe(fs.createWriteStream(`${partId}.tmp`))
    } else {
        throw ("File Can't be Downloaded. Please Try Again.");
    }

})

function uuidv4() {
    //src: https://stackoverflow.com/a/2117523
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
      });
}
const axios = require("axios")
const fs = require('fs')
const args = process.argv
const partId = uuidv4()
const parthPath = __dirname+"\\tmp\\"+`${partId}.tmp`;

//Create TMP if doesn't exsist
if (fs.existsSync(__dirname+"\\tmp") == false) {
    fs.mkdirSync(__dirname+"\\tmp");
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
        res.data.pipe(fs.createWriteStream(parthPath))
        console.log(parthPath)
    } else {
        throw ("File Can't be Downloaded. Please Try Again.");
    }

}).catch(function(err) {
    console.log("error");
})

function uuidv4() {
    //src: https://stackoverflow.com/a/2117523
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
      });
}
const fs = require('fs')
var concat = require('concat-files');
const args = process.argv

// console.log((args[3]))
// process.exit(22);

parts = JSON.parse(args[2])

concat(parts, args[3], function(err) {
  if (err) {
    throw err
  }
  deleteFiles(parts, function(err) {
    if(err) {
      throw err;
    }
    console.log('done');
  })
});

function deleteFiles(files, callback){
  if (files.length==0) callback();
  else {
     var f = files.pop();
     fs.unlink(f, function(err){
        if (err) callback(err);
        else {
           deleteFiles(files, callback);
        }
     });
  }
}

// args[2] = '["D:\\Code\\Node\\DCProject\\Downloader\\utils\\tmp\\05267a84-5279-4ce1-98a4-16c00b5b79c1.tmp","D:\\Code\\Node\\DCProject\\Downloader\\utils\\tmp\\a72e1b87-42c5-40ff-8d63-c0fdec10eddc.tmp"]'
// console.log(args);
// console.log(JSON.parse(args[2]))

// console.log("\n\n---------------------------\n" + JSON.stringify([
//   "D:\\Code\\Node\\DCProject\\Downloader\\utils\tmp\\05267a84-5279-4ce1-98a4-16c00b5b79c1.tmp",
//   "D:\\Code\\Node\\DCProject\\Downloader\\utils\tmp\\05267a84-5279-4ce1-98a4-16c00b5b79c1.tmp"
// ]) + "\n-----------------------")


// concat(JSON.parse(args[2]), 'res.zip', function(err) {
//     if (err) throw err
//     console.log('done');
//   });
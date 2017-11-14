var AWS = require('aws-sdk');

AWS.config.update({
    accessKeyId: "AKIAJZQZ2ZAXPHXPQAYQ",
    secretAccessKey: "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",
    region: 'us-east-2'
});

var s3 = new AWS.S3();

// Bucket names must be unique across all S3 users

var myBucket = 'capstone-team3';

var myKey = 'ea617e859e882e8e3def99ce87656473cd3f2e841ff8d2c4515d3a08be11c7f2';

var uploadParams = {
    Bucket: myBucket,
    Key: myKey,
    Body: ''
};

var fs = require('fs');


var file = '/home/james/GitHub/Capstone2017/server/db/s3/index.js';

var fileStream = fs.createReadStream(file);
fileStream.on('error', function(err) {
    console.log('File Error', err);
});
uploadParams.Body = fileStream;

var path = require('path');
uploadParams.Key = path.basename(file);

// call S3 to retrieve upload file to specified bucket
s3.upload(uploadParams, function(err, data) {
    if (err) {
        console.log("Error", err);
    }
    if (data) {
        console.log("Upload Success", data.Location);
    }
});

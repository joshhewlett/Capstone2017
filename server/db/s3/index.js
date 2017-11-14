let AWS = require('aws-sdk'),
  fs = require('fs'),
  path = require('path');


AWS.config.update({
  accessKeyId: "AKIAJZQZ2ZAXPHXPQAYQ",
  secretAccessKey: "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",
  region: 'us-east-2'
});

let s3 = new AWS.S3();

// Bucket names must be unique across all S3 users

let bucketName = 'capstone-team3';
let bucketKey = 'ea617e859e882e8e3def99ce87656473cd3f2e841ff8d2c4515d3a08be11c7f2';
let uploadParams = {
  Bucket: bucketName,
  Key: bucketKey,
  Body: ''
};

let file = '/home/james/GitHub/Capstone2017/server/db/s3/index.js';

let fileStream = fs.createReadStream(file);
fileStream.on('error', function(err) {
  console.log('File Error', err);
});
uploadParams.Body = fileStream;
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

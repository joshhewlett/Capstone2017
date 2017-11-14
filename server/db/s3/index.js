import {
    DBConfig
} from '../config';

let AWS = require('aws-sdk'),
  fs = require('fs'),
  path = require('path');


AWS.config.update({
    accessKeyId: DBConfig.s3.accessKeyId,
    secretAccessKey: DBConfig.s3.secretAccessKey,
    region: DBConfig.s3.region
});

let s3 = new AWS.S3();

// Bucket names must be unique across all S3 users

var myBucket = DBConfig.s3.bucket;

var myKey = DBConfig.s3.key;

var uploadParams = {
    Bucket: myBucket,
    Key: myKey,
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

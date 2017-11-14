import AWS from 'aws-sdk';
import fs from 'fs';
import path from 'path';
import DBConfig from '../config';

AWS.config.update({
  accessKeyId: DBConfig.s3.accessKeyId,
  secretAccessKey: DBConfig.s3.secretAccessKey,
  region: DBConfig.s3.region
});

let s3 = new AWS.S3();
let myBucket = DBConfig.s3.bucket;
let myKey = DBConfig.s3.key;

async upload(fileStream, path, fileExt) {
  let uploadParams = {
    Bucket: DBConfig.s3.bucket,
    Key: path + fileExt,
    Body: fileStream
  };
  console.log(uploadParams);
  s3.upload(uploadParams, (err, data) => {
    if (err) {
      throw ("S3 Error: ", err);
    }
    console.log("Upload success");
    return data;
  });
}

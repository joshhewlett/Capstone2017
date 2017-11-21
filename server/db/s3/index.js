import AWS from 'aws-sdk';
import fs from 'fs';
import path from 'path';
import DBConfig from '../config';

// AWS S3 configuration
AWS.config.update({
    accessKeyId: DBConfig.s3.accessKeyId,
    secretAccessKey: DBConfig.s3.secretAccessKey,
    region: DBConfig.s3.region
});
let s3 = new AWS.S3();
let myBucket = DBConfig.s3.bucket;
let myKey = DBConfig.s3.key;

export default {
    upload: (fileStream, path) => {
        return new Promise((resolve, reject) => {
            let uploadParams = {
                Bucket: DBConfig.s3.bucket,
                Key: path,
                Body: fileStream
            };
            s3.upload(uploadParams, (err, data) => {
                if (err) {
                    reject(err);
                }
                resolve(data);
            });
        });
    },
    download: (path) => {
        return new Promise((resolve, reject) => {
            s3.getObject({
                Bucket: DBConfig.s3.bucket,
                Key: path
            }, (err, data) => {
                if (err) {
                    reject(err);
                }
                resolve(data);
            });
        })
    },
    deleteObject: (path) => {
        return new Promise((resolve, reject) => {
            s3.deleteObject({
                Bucket: DBConfig.s3.bucket,
                Key: path
            }, (err, data) => {
                if (err) {
                    reject(err);
                }
                resolve(data);
            });
        })
    }
};

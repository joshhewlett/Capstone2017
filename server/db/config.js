// Database configuration file
export default {
    mysql: {
        host: process.env.MYSQL_HOST,
        username: process.env.MYSQL_USERNAME,
        password: process.env.MYSQL_PASSWORD,
        dbs: {
            user: 'User',
            presentation: 'Presentation',
            slide_3d_model: 'Slide_Model',
            slide: 'Slide',
            model: '3D_Model'
        }
    },
    s3: {
        accessKeyId: process.env.S3_ACCESS_KEY_ID,
        secretAccessKey: process.env.S3_SECRET_ACCESS_KEY,
        region: process.env.S3_REGION,
        bucket: process.env.S3_BUCKET,
        key: process.env.S3_KEY
    },
    mongo: {

    }
}

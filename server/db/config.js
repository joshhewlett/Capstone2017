// Database configuration file
export default {
    mysql: {
        host: process.env.MYSQL_HOST,
        port: process.env.MYSQL_PORT,
        username: process.env.MYSQL_USERNAME,
        password: process.env.MYSQL_PASSWORD,
        database: process.env.MYSQL_DB,
        dbs: {
            user: 'Users',
            presentation: 'Presentations',
            slide_3d_model: 'slide_3d_models',
            slide: 'slides',
            model: '3d_models'
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

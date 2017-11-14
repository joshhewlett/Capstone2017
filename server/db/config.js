export let DBConfig = {
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
        accessKeyId: 'AKIAJZQZ2ZAXPHXPQAYQ',
        secretAccessKey: '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',
        region: 'us-east-2',
        bucket: 'capstone-team3',
        key: 'ea617e859e882e8e3def99ce87656473cd3f2e841ff8d2c4515d3a08be11c7f2'
    },
    mongo: {

    }
}

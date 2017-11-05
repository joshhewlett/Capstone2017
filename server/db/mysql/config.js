export let DBConfig = {
  host: process.env.MYSQL_HOST,
  username: process.env.MYSQL_USERNAME,
  password: process.env.MYSQL_PASSWORD,
  dbs: {
    user: 'User',
    presentation: 'Presentation',
    slide_3d_model: 'Slide_3d_Model',
    slide: 'Slide',
    model: '3D_Model'
  }
}

import DBConfig from '../db/config';
import Sequelize from 'sequelize';

const mysqlConfig = DBConfig.mysql;

let sequelize = new Sequelize(mysqlConfig.database, mysqlConfig.username, mysqlConfig.password, {
    host: mysqlConfig.host,
    dialect: 'mysql'
});

sequelize.sync();

export default sequelize;

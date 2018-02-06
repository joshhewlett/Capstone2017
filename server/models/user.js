import Sequelize from 'sequelize';
import sequelize from '../helpers/db.helper';

export default sequelize.define('users', {
    email: {
        type: Sequelize.STRING,
        allowNull: false,
        validate: {
            isEmail: true
        }
    }
}, {
    underscored: true
});

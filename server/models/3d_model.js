import Sequelize from 'sequelize';
import sequelize from '../helpers/db.helper';
import User from './user';

export default sequelize.define('3d_models', {
    path: {
        type: Sequelize.STRING,
        allowNull: false
    },
    soft_delete: {
        type: Sequelize.INTEGER,
        allowNull: false
    },
    user_id: {
        type: Sequelize.INTEGER,
        allowNull: false,
        references: {
            model: User,
            key: 'id'
        }
    },
    name: {
        type: Sequelize.STRING,
        allowNull: false
    }
});

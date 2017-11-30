import Sequelize from 'sequelize';
import sequelize from '../helpers/db.helper';
import User from './user';

export default sequelize.define('presentations', {
    name: {
        type: Sequelize.STRING,
        allowNull: false
    },
    description: {
        type: Sequelize.STRING,
        allowNull: true
    },
    presented: {
        type: Sequelize.INTEGER,
        allowNull: false,
        defaultValue: 0
    },
    user_id: {
        type: Sequelize.INTEGER,
        allowNull: false,
        references: {
            model: User,
            key: 'id'
        }
    }
});

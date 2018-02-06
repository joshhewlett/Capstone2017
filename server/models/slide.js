import Sequelize from 'sequelize';
import sequelize from '../helpers/db.helper';
import Presentation from './presentation';

export default sequelize.define('slides', {
    sequence: {
        type: Sequelize.INTEGER,
        allowNull: false
    }
});

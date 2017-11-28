import Sequelize from 'sequelize';
import sequelize from '../helpers/db.helper';
import Presentation from './presentation';

export default sequelize.define('slides', {
    sequence: {
        type: Sequelize.INTEGER,
        allowNull: false
    },
    presentation_id: {
        type: Sequelize.INTEGER,
        allowNull: false,
        references: {
            model: Presentation,
            key: 'id'
        }
    }
});

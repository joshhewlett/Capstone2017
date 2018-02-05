import Sequelize from 'sequelize';
import sequelize from '../helpers/db.helper';
import Slide from './slide';
import Presentation from './presentation';
import Model from './3d_model';

export default sequelize.define('slide_3d_models', {
    slide_id: {
        type: Sequelize.INTEGER,
        allowNull: false,
        references: {
            model: Slide,
            key: 'id'
        }
    },
    presentation_id: {
        type: Sequelize.INTEGER,
        allowNull: false,
        references: {
            model: Presentation,
            key: 'id'
        }
    },
    poly_id: {
        type: Sequelize.STRING,
        allowNull: false
    },
    /**
     * position: {
     *  x: 0,
     *  y: 0,
     *  z: 0
     * },
     * rotation: {
     *  x: 0,
     *  y: 0,
     *  z: 0
     * },
     * scale: 1
     */
    transform: {
        type: Sequelize.JSON,
        allowNull: false
    }
});

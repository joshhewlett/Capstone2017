import Sequelize from 'sequelize';
import sequelize from '../helpers/db.helper';
import Slide from './slide';
import Presentation from './presentation';

export default sequelize.define('slide_3d_models', {
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
        type: Sequelize.STRING,
        allowNull: true
    }
});

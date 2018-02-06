/**
 * Imports here
 */
import user from './user';
import slide from './slide';
import presentation from './presentation';
import slide_model from './slide_3d_model';

// Relationships
user.hasMany(presentation, {
    as: 'Presentations'
});
presentation.belongsTo(user);

presentation.hasMany(slide, {
    as: 'Slides'
});
slide.belongsTo(presentation);


slide.hasMany(slide_model, {
    as: 'Models'
});
slide_model.belongsTo(slide);


export default {
    user,
    slide,
    presentation,
    slide_model
};

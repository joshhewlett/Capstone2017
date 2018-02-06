/**
 * Imports here
 */
import user from './user';
import slide from './slide';
import presentation from './presentation';
import slide_model from './slide_3d_model';

// Relationships
presentation.hasMany(slide, {
    as: 'Slides'
});
slide.hasMany(slide_model, {
    as: 'Models'
});

export default {
    user,
    slide,
    presentation,
    slide_model
};

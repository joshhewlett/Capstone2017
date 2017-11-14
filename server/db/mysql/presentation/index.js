// Presentation access definition
import create from './create';
import select from './select';
import update from './update';
import deletePresentation from './delete';
export default {
  create,
  select,
  update,
  delete: deletePresentation
}

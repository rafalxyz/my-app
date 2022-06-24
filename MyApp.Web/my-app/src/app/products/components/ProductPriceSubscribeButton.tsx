import { useState } from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import NotificationsActiveIcon from '@mui/icons-material/NotificationsActive';
import {
  Form,
  SaveButton,
  TextInput,
  useCreate,
  useNotify,
  email,
} from 'react-admin';
import { FieldValues } from 'react-hook-form';
import usePriceSubscriptions from '../hooks/usePriceSubscription';

export const ProductPriceSubscribeButton = ({
  productId,
}: {
  productId: string;
}) => {
  const [open, setOpen] = useState(false);

  const notify = useNotify();
  const [create] = useCreate();

  const { subscribe } = usePriceSubscriptions();

  const handleSubmit = async (values: FieldValues) => {
    create(
      'subscriptions',
      { data: { productId, ...values } },
      {
        onSuccess: (data: { id: string }) => {
          subscribe(data.id, productId);
          notify('Subscription created!');
          setOpen(false);
        },
        onError: () => {
          notify('Cannot create subscription.', { type: 'error' });
        },
      }
    );
  };

  return (
    <>
      <Button
        variant="outlined"
        color="primary"
        aria-label="create"
        onClick={() => setOpen(true)}
      >
        <NotificationsActiveIcon sx={{ mr: 1 }} /> Subscribe
      </Button>
      <Dialog
        open={open}
        onClose={() => setOpen(false)}
        aria-labelledby="form-dialog-title"
      >
        <DialogTitle id="form-dialog-title">Subscribe</DialogTitle>
        <DialogContent>
          <Form record={{ email: '' }} onSubmit={handleSubmit}>
            <TextInput
              source="email"
              validate={email('Provide a valid email.')}
              fullWidth
            />
            <SaveButton label="Subscribe" />
          </Form>
        </DialogContent>
      </Dialog>
    </>
  );
};

import Button from '@mui/material/Button';
import NotificationsOffIcon from '@mui/icons-material/NotificationsOff';
import { useNotify, useDelete } from 'react-admin';
import usePriceSubscriptions from '../hooks/usePriceSubscription';

export const ProductPriceUnsubscribeButton = ({
  subscriptionId,
}: {
  subscriptionId: string;
}) => {
  const notify = useNotify();
  const { unsubscribe } = usePriceSubscriptions();

  const [apiDelete] = useDelete();

  const handleClick = async () => {
    apiDelete(
      'subscriptions',
      { id: subscriptionId },
      {
        onSuccess: () => {
          unsubscribe(subscriptionId);
          notify('Unsubscribed!');
        },
        onError: () => {
          notify('Cannot delete subscription.', { type: 'error' });
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
        onClick={() => handleClick()}
      >
        <NotificationsOffIcon sx={{ mr: 1 }} /> Unsubscribe
      </Button>
    </>
  );
};

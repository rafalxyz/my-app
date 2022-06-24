import { useStore } from 'react-admin';
import { Subscription } from '../types';
import { saveSubscriptionsInLocalStorage } from '../utils';

const usePriceSubscriptions = () => {
  const [subscriptions, setSubscriptions] = useStore<Subscription[]>(
    'price-subscriptions',
    []
  );

  const subscribe = (subscriptionId: string, productId: string) => {
    const newSubscriptions = [
      ...subscriptions,
      { id: subscriptionId, productId },
    ];
    setSubscriptions(newSubscriptions);
    saveSubscriptionsInLocalStorage(newSubscriptions);
  };

  const unsubscribe = (subscriptionId: string) => {
    const newSubscriptions = subscriptions.filter(
      (x) => x.id !== subscriptionId
    );
    setSubscriptions(newSubscriptions);
    saveSubscriptionsInLocalStorage(newSubscriptions);
  };

  return {
    subscriptions,
    setSubscriptions,
    subscribe,
    unsubscribe,
  };
};

export default usePriceSubscriptions;

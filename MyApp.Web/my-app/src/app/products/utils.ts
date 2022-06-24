import { Subscription } from './types';

export const getSubscriptionsFromLocalStorage = (): Subscription[] =>
  JSON.parse(localStorage.getItem('PRODUCT_PRICE_SUBSCRIPTIONS') || '[]');

export const saveSubscriptionsInLocalStorage = (
  subscriptions: Subscription[]
) =>
  localStorage.setItem(
    'PRODUCT_PRICE_SUBSCRIPTIONS',
    JSON.stringify(subscriptions)
  );

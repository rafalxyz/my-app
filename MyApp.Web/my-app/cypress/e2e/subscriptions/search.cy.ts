describe('search subscriptions', () => {
  beforeEach(() => {
    cy.intercept(/api\/subscriptions\/search/).as('searchSubscriptions');
    cy.visit('/#/subscriptions');
    cy.wait('@searchSubscriptions');
  });

  it('should display subscriptions', () => {
    cy.get('tbody tr').should('have.length', 10);
  });
});

# Architecture Decision Record: Change payment provider from TPay to Stripe.

## Status

accepted

## Context

We have some problem with TPay. On BigPicture we decide to change payment provider.

## Decision

Use Stripe like payment provider. 
Use Stripe.NET library to handle payments.

## Consequences

We have to remove Payment Controller and use Stripe.NET.
The implementation new feature will be faster.

import { createFileRoute } from '@tanstack/react-router'
import { CafesPage } from '../pages/CafePage'

export const Route = createFileRoute('/cafes')({
  component: () => <CafesPage/>
})
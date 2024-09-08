import { createFileRoute } from '@tanstack/react-router'
import { EmployeePage } from '../pages/EmployeePage'

export const Route = createFileRoute('/employees')({
  component: () => <EmployeePage/>
})
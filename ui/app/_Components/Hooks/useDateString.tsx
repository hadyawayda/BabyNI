import { SelectionRange } from '@progress/kendo-react-dateinputs'

const useCurrentDate = (date: SelectionRange) => {
   const startDate = date.start

   const startYear = startDate!.getFullYear()
   const startMonth = startDate!.getMonth() + 1
   const startDay = startDate!.getDate()

   const start = `${startYear}-${
      startMonth < 10 ? '0' + startMonth : startMonth
   }-${startDay < 10 ? '0' + startDay : startDay}`

   const endDate = date.end

   const endYear = endDate!.getFullYear()
   const endMonth = endDate!.getMonth() + 1
   const endDay = endDate!.getDate()

   const end = `${endYear}-${endMonth < 10 ? '0' + endMonth : endMonth}-${
      endDay < 10 ? '0' + endDay : endDay
   }`

   return { start, end }
}

export default useCurrentDate

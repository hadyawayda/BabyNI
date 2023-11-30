const useCurrentDate = () => {
   const now = new Date()

   const currentYear = now.getFullYear()
   const currentMonth = now.getMonth() + 1
   const currentDay = now.getDate()

   const today = `${currentYear}-${
      currentMonth < 10 ? '0' + currentMonth : currentMonth
   }-${currentDay < 10 ? '0' + currentDay : currentDay}`

   const oneMonthAgo = new Date()
   oneMonthAgo.setMonth(now.getMonth() - 1)
   const yearOneMonthAgo = oneMonthAgo.getFullYear()
   const monthOneMonthAgo = oneMonthAgo.getMonth() + 1
   const dayOneMonthAgo = oneMonthAgo.getDate()

   const aMonthAgo = `${yearOneMonthAgo}-${
      monthOneMonthAgo < 10 ? '0' + monthOneMonthAgo : monthOneMonthAgo
   }-${dayOneMonthAgo < 10 ? '0' + dayOneMonthAgo : dayOneMonthAgo}`

   const start = aMonthAgo
   const end = today

   return [start, end]
}

export default useCurrentDate

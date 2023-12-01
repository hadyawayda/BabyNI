import getGridData from './_Components/Data/GridData'
import getChartData from './_Components/Data/ChartData'
import Body from './_Components/Body'

const App = async () => {
   const gridData = await getGridData({ dateRange: 'daily' })
   const chartData = await getChartData()

   return (
      <>
         <Body
            gridData={gridData}
            chartData={chartData}
         />
      </>
   )
}

export default App

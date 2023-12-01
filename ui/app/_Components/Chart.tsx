import {
   Chart,
   ChartTitle,
   ChartSeries,
   ChartSeriesItem,
   ChartCategoryAxis,
   ChartCategoryAxisItem,
} from '@progress/kendo-react-charts'
import { ChartComponentProps as Props } from './Interfaces/Interfaces'
import { useEffect, useState } from 'react'
import DateTimeKeySelector from './DateTimeKeySelector'

const ChartComponent = ({
   data,
   grouping,
   selectedKPIs,
   dateTimeKeys,
   onDateTimeKeyChange,
}: Props) => {
   // const [dateTimeKeys, setDateTimeKeys] = useState([])
   const [categories, setCategories] = useState<Set<string>>(new Set())
   const [inputPower, setInputPower] = useState<number[]>([])
   const [maxRxLevel, setMaxRxLevel] = useState<number[]>([])
   const [rslDeviation, setRslDeviation] = useState<number[]>([])

   function fetchData() {
      const newInputPower: number[] = []
      const newMaxRxLevel: number[] = []
      const newRslDeviation: number[] = []

      data?.forEach((x) => {
         newInputPower.push(x.RSL_INPUT_POWER)
         newMaxRxLevel.push(x.MAX_RX_LEVEL)
         newRslDeviation.push(x.RSL_DEVIATION)
      })

      setInputPower(newInputPower)
      setMaxRxLevel(newMaxRxLevel)
      setRslDeviation(newRslDeviation)
   }

   useEffect(() => fetchData(), [data])

   function fetchCategories() {
      let set = new Set<string>()
      let group = ''

      if (grouping === 'NETYPE') group = 'NETYPE'
      if (grouping === 'NEALIAS') group = 'NEALIAS'

      data?.forEach((x) => {
         set.add(group === 'NEALIAS' ? x.NEALIAS : x.NETYPE)
      })

      setCategories(set)
   }

   // Add fetch function that fetches individual timeframes and set them to the categories array

   useEffect(() => fetchCategories(), [grouping])

   return (
      <>
         {/* 3andak granularity bel data (total individual dates) men wara l interval selection aktar mafi individual types (NETYPE or NEALIAS) */}
         <Chart className="w-11/12 mt-4 mb-6">
            <div className="flex justify-around">
               <ChartTitle
                  margin={10}
                  text="Performance Chart"
               />
            </div>
            <ChartCategoryAxis>
               <ChartCategoryAxisItem
               // title={{ text: 'KPIs' }}
               // categories={[...categories]}
               />
            </ChartCategoryAxis>
            <ChartSeries>
               {selectedKPIs.get('RSL_INPUT_POWER') && (
                  <ChartSeriesItem
                     name={'RSL_INPUT_POWER'}
                     type="line"
                     data={inputPower}
                  />
               )}
               {selectedKPIs.get('MAX_RX_LEVEL') && (
                  <ChartSeriesItem
                     name={'MAX_RX_LEVEL'}
                     type="line"
                     data={maxRxLevel}
                  />
               )}
               {selectedKPIs.get('RSL_DEVIATION') && (
                  <ChartSeriesItem
                     name={'RSL_DEVIATION'}
                     type="line"
                     data={rslDeviation}
                  />
               )}
            </ChartSeries>
         </Chart>
         {/* <DateTimeKeySelector {...{ dateTimeKeys, onDateTimeKeyChange }} /> */}
      </>
   )
}

export default ChartComponent

// {
//    /* <ul>
//          {Object.entries(selectedKPIs).map(([key, value], i) => (
//             <li key={i}>{value ? key : null}</li>
//          ))}
//       </ul> */
// }
// useEffect(() => {
//    console.log(selectedKPIs)
// }, [selectedKPIs])

{
   /* {[...selectedKPIs].map(([key, value]) => {
                  return (
                     <div key={key}>
                        {value && (
                           <ChartSeriesItem
                              // key={key}
                              name={key}
                              type="line"
                              data={[-20]}
                           />
                        )}
                     </div>
                  )
               })} */
}
{
   /* {chartData.map((item, idx) => (
                  <ChartSeriesItem
                     key={idx}
                     type="line"
                     data={[
                        item.RSL_INPUT_POWER,
                        item.MAX_RX_LEVEL,
                        item.RSL_DEVIATION,
                     ]}
                     name={legend}
                  />
               ))} */
}

// useEffect(() => {
//    const enabledCategories = Object.entries(selectedKPIs)
//       .filter(([key, value]) => value) // Keep only entries where the value is true
//       .map(([key]) => key) // Extract the keys

//    setCategories(enabledCategories)
// }, [selectedKPIs])

// useEffect(() => console.log(categories), [selectedKPIs])

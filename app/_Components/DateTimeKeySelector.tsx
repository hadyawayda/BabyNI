import { Fragment, useState } from 'react'
import { ReactChange } from './Interfaces/Interfaces'
import { Dialog, Transition } from '@headlessui/react'

const DateTimeKeySelector = ({
   dateTimeKeys,
   onDateTimeKeySelect,
}: {
   dateTimeKeys: object
   onDateTimeKeySelect: (selection: ReactChange) => void
}) => {
   let [isOpen, setIsOpen] = useState(false)
   return (
      <>
         <button
            className="whitespace-nowrap rounded-md bg-gray-600 px-4 py-2 text-sm font-medium text-white hover:bg-orange-600 transition-colors duration-200"
            onClick={() => setIsOpen(true)}
         >
            Select Grouping
         </button>
         <Transition
            appear
            show={isOpen}
            as={Fragment}
         >
            <Dialog
               as="div"
               className="relative z-10"
               onClose={() => setIsOpen(false)}
            >
               <Transition.Child
                  as={Fragment}
                  enter="ease-out duration-300"
                  enterFrom="opacity-0"
                  enterTo="opacity-100"
                  leave="ease-in duration-200"
                  leaveFrom="opacity-100"
                  leaveTo="opacity-0"
               >
                  <div
                     className="fixed inset-0 bg-slate-950/60"
                     aria-hidden="true"
                  />
               </Transition.Child>
               <div className="fixed inset-0 overflow-y-auto">
                  <div className="flex min-h-full items-center justify-center p-4 text-center">
                     <Transition.Child
                        as={Fragment}
                        enter="ease-out duration-300"
                        enterFrom="opacity-0 scale-95"
                        enterTo="opacity-100 scale-100"
                        leave="ease-in duration-200"
                        leaveFrom="opacity-100 scale-100"
                        leaveTo="opacity-0 scale-95"
                     >
                        <Dialog.Panel className="text-black relative mx-auto p-9 rounded-2xl flex flex-col justify-start items-center bg-slate-100 dialog-panel">
                           <div className="flex flex-col justify-start items-center">
                              <Dialog.Title className="mb-4">
                                 Select KPIs:
                              </Dialog.Title>

                              <div className="mt-4">
                                 Select DateTime Keys:
                                 <div className=" flex flex-col justify-center gap-1.5 mt-4">
                                    {/* {dateTimeKeys.map((x) => (
                               <div className="flex ml-4">
                                  <input
                                     type="checkbox"
                                     name={x.toString()}
                                     checked={Object.values(x)[0]}
                                     onChange={handleDateTimeKeySelect}
                                  />
                                  <label className="flex ml-8 text-black m-4">
                                     {Object.keys(x)}
                                  </label>
                               </div>
                            ))} */}
                                    {Object.entries(dateTimeKeys).map(
                                       ([key, value]) => (
                                          <div
                                             className="flex ml-4 h-8"
                                             key={key}
                                          >
                                             <input
                                                type="checkbox"
                                                name={key}
                                                checked={value}
                                                onChange={onDateTimeKeySelect}
                                             />
                                             <label className="ml-6 flex justify-center items-center">
                                                {key}
                                             </label>
                                          </div>
                                       )
                                    )}
                                 </div>
                              </div>
                           </div>
                        </Dialog.Panel>
                     </Transition.Child>
                  </div>
               </div>
            </Dialog>
         </Transition>
      </>
   )
}

export default DateTimeKeySelector
